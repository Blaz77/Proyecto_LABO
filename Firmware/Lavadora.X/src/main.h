/* 
 * File:   main.h
 * Author: Admin
 *
 * Created on 19 de marzo de 2017, 20:59
 */

/*#ifndef MAIN_H
#define MAIN_H*/

#include <16F628A.h>

#FUSES NOWDT                    //No Watch Dog Timer
#FUSES NOPUT                      //Power Up Timer
#FUSES NOMCLR                   //Master Clear pin used for I/O
#FUSES NOBROWNOUT               //No brownout reset
#FUSES NOLVP                    //No low voltage prgming, B3(PIC16) or B5(PIC18) used for I/O
#FUSES NOCPD                    //No EE protection
#FUSES PROTECT                  //Code protected from reads

#use fast_io(a)
#use fast_io(b)
#use delay(internal=4000000)

#define IN_ON           FALSE
#define IN_OFF          TRUE

#define OUT_ON          FALSE
#define OUT_OFF         TRUE

#define E_RESET_ALARMA  pin_a2
#define E_NIVEL_MIN     pin_a6
#define E_NIVEL_MAX     pin_a7
#define E_BOTON_CICLO   pin_a0
#define E_BOTON_CENTRI  pin_a1
#define E_PAUSA         pin_a3

#define S_ALARMA        pin_b7
#define S_CARGA_LAVADO  pin_b6
#define S_CARGA_ENJUAG  pin_b5
#define S_GIRO_IZQ      pin_b4
#define S_GIRO_DER      pin_b3
#define S_GIRO_CENTRI   pin_b2
#define S_DESCARGA      pin_b1
#define S_FIN           pin_b0

#define TIEMPO_GIRO     15      //seg
#define TIEMPO_DESACEL  3       //seg
#define CARGA_TIMEOUT   7      //minutos
#define CICLOS_LAVADO   18      //Un ciclo incluye ambos giros y desaceleraciones
#define CICLOS_ENJUAG   12
#define CICLOS_DESC_A   6       //Ciclos de descarga maximos previos al nivel minimo
#define CICLOS_DESC_B   4       //Ciclos de descarga luego del nivel minimo
#define T_CENTRI_LAVADO 2       //minutos
#define T_CENTRI_FINAL  4       //minutos
#define PARADA_CENTRI   10      //segundos


enum tipoPrograma {
    EN_ESPERA, CICLO_NORMAL, CENTRIFUGADO
};

enum etapaPrograma {
    CARGA_LAVADO, LAVANDO, DESCARGA_LAVADO, CENTRI_LAVADO,
    CARGA_ENJUAGUE, ENJUAGANDO, DESCARGA_ENJUAGUE, CENTRI_FINAL,
    TERMINADO
};

enum etapaGiro {
    GIRO_HORARIO, DESACEL_HOR, GIRO_ANTIHOR, DESACEL_ANTIHOR
};

//#endif  // MAIN_H
