import { CanActivate, Router, CanLoad, Route, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { NbAuthService } from "@nebular/auth";
import { tap } from "rxjs/operators";
import { Observable } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {

    constructor(private authService: NbAuthService, 
        private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.checkActivate();
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.canActivate(route, state);
    }

    canLoad(route: Route): boolean {
        return true;
    }

    checkActivate(): Observable<boolean> {
        return this.authService.isAuthenticated()
        .pipe(
            tap(authenticated=>{
                if(!authenticated){
                    this.router.navigate(["auth/login"]);
                }
            })
        );
    }
}