


// export class ValidaCNPJ {
//     public static isCNPJ( CNPJ: string): boolean {
//         // considera-se erro CNPJ's formados por uma sequencia de numeros iguais
//         if (CNPJ === '00000000000000' || CNPJ === '11111111111111' ||
//             CNPJ === '22222222222222' || CNPJ === '33333333333333' ||
//             CNPJ === '44444444444444' || CNPJ === '55555555555555' ||
//             CNPJ === '66666666666666' || CNPJ === '77777777777777' ||
//             CNPJ === '88888888888888' || CNPJ === '99999999999999' ||
//             (CNPJ.length !== 14)) {
//                 return (false);
//             }

//         let dig13;
//         let dig14;
//         let sm;
//         let i;
//         let r;
//         let num;
//         let peso;


//         try {

//             sm = 0;
//             peso = 2;
//             for (i = 11; i >= 0; i--) {

//                 num = (CNPJ.charAt(i) - 48);
//                 sm = sm + (num * peso);
//                 peso = peso + 1;
//                 if (peso == 10)
//                     peso = 2;
//             }

//             r = sm % 11;
//             if ((r == 0) || (r == 1))
//                 dig13 = '0';
//             else dig13 = (char)((11 - r) + 48);

//             // Calculo do 2o. Digito Verificador
//             sm = 0;
//             peso = 2;
//             for (i = 12; i >= 0; i--) {
//                 num = (int)(CNPJ.charAt(i) - 48);
//                 sm = sm + (num * peso);
//                 peso = peso + 1;
//                 if (peso == 10)
//                     peso = 2;
//             }

//             r = sm % 11;
//             if ((r == 0) || (r == 1))
//                 dig14 = '0';
//             else dig14 = (char)((11 - r) + 48);

//             // Verifica se os dígitos calculados conferem com os dígitos informados.
//             if ((dig13 == CNPJ.charAt(12)) && (dig14 == CNPJ.charAt(13)))
//                 return (true);
//             else return (false);
//         } catch (InputMismatchException erro) {
//             return (false);
//         }
//     }

//     public static String imprimeCNPJ(String CNPJ) {
//         // máscara do CNPJ: 99.999.999.9999-99
//         return (CNPJ.substring(0, 2) + "." + CNPJ.substring(2, 5) + "." +
//             CNPJ.substring(5, 8) + "/" + CNPJ.substring(8, 12) + "-" +
//             CNPJ.substring(12, 14));
//     }
// }
