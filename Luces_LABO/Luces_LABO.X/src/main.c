#include "main.h"

void main()
{
	int i=0, d=0;
	int Periodo = 20; // decimilisegundos
	int Activo = 0;
	int Inactivo = 20;
	short Mas=0, Menos=0;
    int cant_periodos = 0; // 1 periodo = 2 ms
	
	
   setup_timer_0(RTCC_INTERNAL|RTCC_DIV_1);
   setup_timer_1(T1_DISABLED);
   setup_timer_2(T2_DISABLED,0,1);
   setup_comparator(NC_NC_NC_NC);
   setup_vref(FALSE);
//Setup_Oscillator parameter not selected from Intr Oscillator Config tab

	while(TRUE) {
		if (Activo == 0) {
			output_low(PWM);
			delay_ms(50);
            cant_periodos += 25;
		}
		else if (Activo == Periodo) {
			output_high(PWM);
			delay_ms(50);
            cant_periodos += 25;
		}
		else {
            // 25 periodos = 50ms aprox
			for (i=0; i <25; i++) {
				output_high(PWM);
				for (d=0; d<Activo; d++) {
					delay_us(93);
				}
				output_low(PWM);
				for (d=0; d<Inactivo; d++) {
					delay_us(93);
				}
			}
            cant_periodos += 25;
		}
		Mas = input(P_MAS);
		Menos = input(P_MENOS);
		if (Mas == 1 && Menos == 1) {
			
		}
		else if (Mas == 1) {
			if (Activo < Periodo) {
				Activo++;
				Inactivo = Periodo - Activo;
			}
		}
		else if (Menos == 1) {
			if (Activo > 0) {
				Activo--;
				Inactivo = Periodo - Activo;
			}
		}
        
        if (cant_periodos >= 200) {
            output_toggle(S_LED2);
            cant_periodos = 0;
        }
	}	

}
