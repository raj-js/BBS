import { Apis } from './Apis';
import { NbAuthJWTToken } from '@nebular/auth';

export class User {
    public Id: string;
    public Nickname: string;
    public Email: string;
    public IsValid = false;

    constructor() {
    }

    public static FromJwt(token: NbAuthJWTToken): User {
        const user = new User();
        if (token.isValid()) {
            const payload = token.getPayload();
            user.Id = payload[Apis.ClaimTypes.Id];
            user.Nickname = payload['Nickname'];
            user.Email = payload[Apis.ClaimTypes.Email];
            user.IsValid = true;
            Apis.Authorize.AuthToken = token.getValue();
        }
        return user;
    }
}
