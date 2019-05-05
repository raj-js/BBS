import { Apis } from './Config';
import { HttpHeaders } from '@angular/common/http';

export class BaseClient {
    protected transformOptions(options: any): any {
        options.headers = (<HttpHeaders> (options.headers)).append('Authorization', `bearer ${Apis.AccessToken}`);

        // 不要使用 Promise.resolve, 原因详见 Observerable 与 Promise
        return new Array(...options);
    }
}
