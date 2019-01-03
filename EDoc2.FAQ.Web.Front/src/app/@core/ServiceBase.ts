import { Apis } from './Apis';
import { HttpHeaders } from '@angular/common/http';

export class ServiceBase {
    public transformOptions(options: any): any {
        if (Apis.Authorize.AuthToken != null) {
            options.headers = (<HttpHeaders> (options.headers)).append('Authorization', `bearer ${(Apis.Authorize.AuthToken)}`);
        }
        return new Array(...options);
    }

    public getBaseUrl(baseUrl: string): string {
        return Apis.BASE_URL;
    }
}
