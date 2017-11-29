/* 
 * File:   main.c
 * Author: Admin
 *
 * Created on 19 de marzo de 2017, 20:59
 */

#include <main.h>

#include <1wire.c>
#include <ds1820.c>

// Variables configurables
int TIEMPO_GIRO = 1;		// segundos
int CARGA_TIMEOUT = 1;		// minutos

// Variables de estado
 short est_resetalarma = IN_OFF;
 short est_nivel_min = IN_OFF;
 short est_nivel_max = IN_OFF;
 short est_boton_ciclo = IN_OFF;
 //short est_boton_centri = IN_OFF;
 //short est_pausa = IN_OFF;
 tipoPrograma programa = EN_ESPERA;
 etapaPrograma etapa = TERMINADO;
 etapaGiro giro = GIRO_ANTIHOR;
 short activar_alarma = FALSE;
 short reset_tiempo = FALSE;
 int segundos = 0, minutos = 0;
 int ciclos_hechos = 0;
 int in_data[2];
 int out_data[4];
 int temperatura = TEMPERATURA_SET;

void ScanEntradas()
{
    est_resetalarma = input(E_RESET_ALARMA);
    est_nivel_min = input(E_NIVEL_MIN);
    est_nivel_max = input(E_NIVEL_MAX);
    est_boton_ciclo = input(E_BOTON_CICLO);
    //est_boton_centri = input(E_BOTON_CENTRI);
    //est_pausa = input(E_PAUSA);
}

void ResetTiempos()
{
    segundos = 0;
    minutos = 0;
}

void PrepararCentrifugado() 
{
    // Periodo critico, se anula la pausa
    output_bit(S_FIN, OUT_OFF);
    output_bit(S_GIRO_DER, OUT_ON);
    for (int i=0; i<TIEMPO_GIRO; i++) {
        delay_ms(1000);
    }
    output_bit(S_GIRO_DER, OUT_OFF);
    delay_ms(800);
}

void DefinirPrograma()
{
    if (programa == EN_ESPERA) {
        if (est_boton_ciclo == IN_ON) {
            programa = CICLO_NORMAL;
            etapa = CARGA_LAVADO;
        }
//        else if (est_boton_centri == IN_ON) {
//            PrepararCentrifugado();
//            programa = CENTRIFUGADO;
//            etapa = CENTRI_FINAL;
//        }
    }
    
}

void MantenerEspera()
{
    output_bit(S_FIN, OUT_ON);
}

void EjecutarGiros()
{
    if (giro == GIRO_ANTIHOR) {
        if (segundos >= TIEMPO_GIRO) {
            giro = DESACEL_ANTIHOR;
            ResetTiempos();
        }
        else {
            output_bit(S_GIRO_IZQ, OUT_ON);
        }
    }
    else if (giro == DESACEL_ANTIHOR) {
        if (segundos >= TIEMPO_DESACEL) {
            giro = GIRO_HORARIO;
            ResetTiempos();
        }
        else {
            output_bit(S_GIRO_IZQ, OUT_OFF);
        }
    }
    else if (giro == GIRO_HORARIO) {
        if (segundos >= TIEMPO_GIRO) {
            giro = DESACEL_HOR;
            ResetTiempos();
        }
        else {
            output_bit(S_GIRO_DER, OUT_ON);
        }
    }
    else if (giro == DESACEL_HOR) {
        if (segundos >= TIEMPO_DESACEL) {
            giro = GIRO_ANTIHOR;
            ResetTiempos();
            ciclos_hechos++;
        }
        else {
            output_bit(S_GIRO_DER, OUT_OFF);
        }
    }
}

void EjecutarCicloNormal()
{
    if (etapa == CARGA_LAVADO) {
        output_bit(S_FIN, OUT_OFF);
        output_bit(S_DESCARGA, OUT_ON); //La valvula descarga en OFF
        if (est_nivel_max == IN_ON) {
            output_bit(S_CARGA_LAVADO, OUT_OFF);
            etapa = LAVANDO;
            giro = GIRO_ANTIHOR;
            reset_tiempo = TRUE;
        }
        else {
            if (minutos >= CARGA_TIMEOUT) {
                output_bit(S_CARGA_LAVADO, OUT_OFF);
                activar_alarma = TRUE;
            }
            else if (segundos > 0) {
                output_bit(S_CARGA_LAVADO, OUT_ON);
            }
        }
    }
    else if (etapa == LAVANDO) {
        if (est_nivel_max == IN_ON) 
            output_bit(S_CARGA_LAVADO, OUT_OFF);
        else
            output_bit(S_CARGA_LAVADO, OUT_ON);
        EjecutarGiros();
        if (ciclos_hechos >= CICLOS_LAVADO) {
            ciclos_hechos = 0;
            output_bit(S_CARGA_LAVADO, OUT_OFF);
            etapa = DESCARGA_LAVADO;
        }
    }
    else if (etapa == DESCARGA_LAVADO) {
        if (segundos > 0) 
            output_bit(S_DESCARGA, OUT_OFF);    //La valvula descarga en OFF
        EjecutarGiros();
        if (ciclos_hechos < CICLOS_DESC_A && est_nivel_min == IN_OFF) {
            ciclos_hechos = CICLOS_DESC_A;
        }
        else if (ciclos_hechos == CICLOS_DESC_A && est_nivel_min == IN_ON) {
            ciclos_hechos = 0;
            activar_alarma = TRUE;
        }
        else if (ciclos_hechos >= CICLOS_DESC_A + CICLOS_DESC_B) {
            ciclos_hechos = 0;
            ResetTiempos();
            PrepararCentrifugado();
            output_bit(S_GIRO_CENTRI, OUT_ON);
            etapa = CENTRI_FINAL;
        }
    }
    /* else if (etapa == CENTRI_LAVADO) {
        output_bit(S_GIRO_CENTRI, OUT_ON);
        if (minutos >= T_CENTRI_LAVADO) {
            output_bit(S_GIRO_CENTRI, OUT_OFF);
            ResetTiempos();
            etapa = CARGA_ENJUAGUE;
            // Carga prematura mientras se detiene
            output_bit(S_CARGA_ENJUAG, OUT_ON);
            delay_ms(PARADA_CENTRI * 300);
            output_bit(S_DESCARGA, OUT_ON); //La valvula descarga en OFF
            delay_ms(PARADA_CENTRI * 700);
        }
    }
    else if (etapa == CARGA_ENJUAGUE) {
        output_bit(S_DESCARGA, OUT_ON); //La valvula descarga en OFF
        if (est_nivel_max == IN_ON) {
            output_bit(S_CARGA_ENJUAG, OUT_OFF);
            output_bit(S_CARGA_LAVADO, OUT_OFF);
            etapa = ENJUAGANDO;
            giro = GIRO_ANTIHOR;
            reset_tiempo = TRUE;
        }
        else {
            if (minutos >= CARGA_TIMEOUT) {
                output_bit(S_CARGA_ENJUAG, OUT_OFF);
                output_bit(S_CARGA_LAVADO, OUT_OFF);
                activar_alarma = TRUE;
            }
            else if (segundos > 0) {
                output_bit(S_CARGA_ENJUAG, OUT_ON);
                output_bit(S_CARGA_LAVADO, OUT_ON);
            }
        }
    }
    else if (etapa == ENJUAGANDO) {
        if (est_nivel_max == IN_ON) 
            output_bit(S_CARGA_ENJUAG, OUT_OFF);
        else
            output_bit(S_CARGA_ENJUAG, OUT_ON);
        EjecutarGiros();
        if (ciclos_hechos >= CICLOS_ENJUAG) {
            ciclos_hechos = 0;
            output_bit(S_CARGA_ENJUAG, OUT_OFF);
            output_bit(S_DESCARGA, OUT_OFF);    //La valvula descarga en OFF
            etapa = DESCARGA_ENJUAGUE;
        }
    }
    else if (etapa == DESCARGA_ENJUAGUE) {
        output_bit(S_DESCARGA, OUT_OFF);    //La valvula descarga en OFF
        EjecutarGiros();
        if (ciclos_hechos < CICLOS_DESC_A && est_nivel_min == IN_OFF) {
            ciclos_hechos = CICLOS_DESC_A;
        }
        else if (ciclos_hechos == CICLOS_DESC_A && est_nivel_min == IN_ON) {
            ciclos_hechos = 0;
            activar_alarma = TRUE;
        }
        else if (ciclos_hechos >= CICLOS_DESC_A + CICLOS_DESC_B) {
            ciclos_hechos = 0;
            ResetTiempos();
            PrepararCentrifugado();
            output_bit(S_GIRO_CENTRI, OUT_ON);
            etapa = CENTRI_FINAL;
        }
    } */
    else if (etapa == CENTRI_FINAL) {
        output_bit(S_GIRO_CENTRI, OUT_ON);
        if (minutos >= T_CENTRI_FINAL) {
            output_bit(S_GIRO_CENTRI, OUT_OFF);
            reset_tiempo = TRUE;
            delay_ms(PARADA_CENTRI * 1000);
            etapa = TERMINADO;
            programa = EN_ESPERA;
        }
    }
}

void EjecutarCentrifugado()
{
    output_bit(S_GIRO_CENTRI, OUT_ON);
    if (minutos >= T_CENTRI_FINAL) {
        output_bit(S_GIRO_CENTRI, OUT_OFF);
        reset_tiempo = TRUE;
        delay_ms(PARADA_CENTRI * 1000);
        etapa = TERMINADO;
        programa = EN_ESPERA;
    }
}

void GenerarAlarma()
{
    /* Causantes de alarma:
     * - Al iniciar se detecta agua
     * - Se excedio el tiempo limite de carga de agua 
     * - Se excedio el tiempo limite de descarga 
     */
    output_bit(S_ALARMA, OUT_ON);
    ResetTiempos();
    while (input(E_RESET_ALARMA) != IN_ON) {
        delay_ms(150);
    }
    activar_alarma = FALSE;
    output_bit(S_ALARMA, OUT_OFF);
}

//void PausarCiclo()
//{
//    output_bit(S_CARGA_LAVADO, OUT_OFF);
//    output_bit(S_CARGA_ENJUAG, OUT_OFF);
//    output_bit(S_GIRO_IZQ, OUT_OFF);
//    output_bit(S_GIRO_DER, OUT_OFF);
//    output_bit(S_GIRO_CENTRI, OUT_OFF);
//    
//    for (int i=0; i<3; i++) {
//        output_bit(S_ALARMA, OUT_ON);
//        delay_ms(800);
//        output_bit(S_ALARMA, OUT_OFF);
//        delay_ms(800);
//    }
//    while (input(E_PAUSA) != IN_OFF) {
//        delay_ms(200);
//    }
//    while (input(E_PAUSA) != IN_ON) {
//        delay_ms(200);
//    }
//    while (input(E_PAUSA) != IN_OFF) {
//        delay_ms(200);
//    }
//    if (etapa == CENTRI_LAVADO || etapa == CENTRI_FINAL) {
//        PrepararCentrifugado();
//    }
//}

void main()
{
    setup_timer_0(RTCC_INTERNAL|RTCC_DIV_1);
	setup_timer_1(T1_DISABLED);
	setup_timer_2(T2_DISABLED,0,1);
	setup_comparator(NC_NC_NC_NC);
	setup_vref(FALSE);
    
    set_tris_a(0b11110111);
	set_tris_b(0b00000000);
    
    output_low(PIN_TXD);
    //output_a(0x00);
	output_b(0b11111111);
    
	disable_interrupts(INT_EXT);
	disable_interrupts(GLOBAL);  
    
    delay_ms(400);
    
    while (input(E_NIVEL_MAX) == IN_ON || input(E_NIVEL_MIN) == IN_ON)
        GenerarAlarma();
    
	TIEMPO_GIRO = read_eeprom(EEPROM_TIEMPO_GIRO);
	CARGA_TIMEOUT = read_eeprom(EEPROM_CARGA_TIMEOUT);
	
    delay_ms(500);
    output_bit(S_FIN, OUT_ON);
    while(TRUE)
    {
        
        ScanEntradas();
        if (activar_alarma)
            GenerarAlarma();
//        if (est_pausa == IN_ON && programa != EN_ESPERA)
//            PausarCiclo();
        DefinirPrograma();
        if (programa == EN_ESPERA) {
            MantenerEspera();
        }
        else if (programa == CICLO_NORMAL) {
            EjecutarCicloNormal();
        }
        else if (programa == CENTRIFUGADO) {
            EjecutarCentrifugado();
        }
        if (programa == EN_ESPERA) {
            delay_ms(150);
        }
        else {
            temperatura = ds1820_read();
            if (temperatura < 30) 
                output_bit(S_CALENTADOR, OUT_ON);
            else
                output_bit(S_CALENTADOR, OUT_OFF);
            out_data[O_TEMPERATURA] = temperatura;
            out_data[O_ESTADO] = etapa;
            out_data[O_T_CARGA] = CARGA_TIMEOUT;
            out_data[O_T_GIRO] = TIEMPO_GIRO;
            for (int i=0; i<4; i++) {
                putc(out_data[i]);
            }
            delay_ms(899);
            if (reset_tiempo) {
                ResetTiempos();
                reset_tiempo = FALSE;
            }
            else {
                segundos++;
                if (segundos == 60) {
                    minutos++;
                    segundos = 0;
                }
            }
        }
    }
    
}

