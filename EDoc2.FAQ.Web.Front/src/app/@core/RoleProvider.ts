import { Injectable } from '@angular/core';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { NbRoleProvider } from '@nebular/security';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apis } from './Apis';

@Injectable()
export class RoleProvider implements NbRoleProvider {

  constructor(private authService: NbAuthService) {
  }

  getRole(): Observable<string> {
    return this.authService.onTokenChange()
      .pipe(
        map((token: NbAuthJWTToken) => {
          console.log('RoleProvider:', token);
            if (token.isValid()) {
                Apis.Authorize.AuthToken = token;
                Apis.Authorize.IsSignIn = true;
                Apis.Authorize.UserId = token.getPayload()[Apis.ClaimTypes.Id];
                return token.getPayload()[Apis.ClaimTypes.Role];
            } else {
              Apis.Authorize.AuthToken = null;
              return 'GUEST';
            }
        }),
      );
  }
}
