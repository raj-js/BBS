import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fdyEmpty'
})
export class FdyEmptyPipe implements PipeTransform {

  transform(value: string, dft?: string): any {

    if (dft === null || dft === undefined) {
      dft = '未知';
    }

    if (value == null) {
      return dft;
    }

    if (value.length === 0) {
      return dft;
    }

    return value;
  }

}
