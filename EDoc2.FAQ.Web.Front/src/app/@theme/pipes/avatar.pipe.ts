import { Pipe, PipeTransform } from '@angular/core';
import { Apis } from 'src/app/@core/Apis';

@Pipe({
  name: 'avatar'
})
export class AvatarPipe implements PipeTransform {

  transform(value: string, args?: any): any {
    return Apis.AvatarUrl + value;
  }

}
