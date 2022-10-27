import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'custom'
})
export class CustomPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    // Defino la transformación, Ojo: Fijarse en la doc si ya eciste lo que quiero hacer relacionado a pipes
    // Cuando usamos el pipe, podemos pasar mas argumentos extras, vienen en args el cual podemos tipar
    if(value != undefined){
      value = value.toString().toUpperCase()
      value = `__${value}__`
    }
    return value;
  }

}
