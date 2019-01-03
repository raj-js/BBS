import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'fdyTime'
})
export class FdyTimePipe implements PipeTransform {

  transform(value: Date, args?: any): any {
    const target = moment(value);

    // 年份不一致
    if (moment().format('YYYY') !== target.format('YYYY')) {
      return target.format('YYYY年MM月DD HH:mm');
    }

    // 10 分钟内
    if (moment().subtract(10, 'minute').isBefore(target)) {
      return '刚刚';
    }

    // 一小时内
    if (moment().subtract(1, 'hour').isBefore(target)) {
      return `${moment().minute() - target.minute()}分钟之前`;
    }

    // 时间差在一天内
    if (moment().subtract(1, 'day').isBefore(target)) {
      // 但不是同一天
      if (moment().format('DD') !== target.format('DD')) {
        return target.format('昨日 HH:mm');
      }
      return target.format('HH:mm');
    }

    // 时差在一年内
    if (moment().subtract(1, 'year').isBefore(target)) {
      // 但不是同一年
      if (moment().format('YYYY') !== target.format('YYYY')) {
        return target.format('YYYY年MM月DD HH:mm');
      }
      return target.format('MM月DD日 HH:mm');
    }

    return target.format('YYYY年MM月DD HH:mm');
  }
}
