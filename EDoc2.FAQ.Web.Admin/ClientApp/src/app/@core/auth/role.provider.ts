import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators/map';

import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { NbRoleProvider } from '@nebular/security';
import { ClaimTypes } from '../data/ClaimTypes';
import { Apis } from '../data/Config';

@Injectable()
export class RoleProvider implements NbRoleProvider {

    constructor(private authService: NbAuthService) {
    }

    getRole(): Observable<string> {
        return this.authService.onTokenChange()
            .pipe(
                map((token: NbAuthJWTToken) => {
                    if(token.isValid()){
                        Apis.AccessToken = token.getValue();
                    }
                    return token.isValid() ? token.getPayload()[ClaimTypes.Role] : 'guest';
                }),
            );
    }
}