import {Injectable} from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot} from '@angular/router';
import { LoginService } from './login.service';

@Injectable({
providedIn: 'root'
})

export class AuthGuard implements CanActivate {
   
constructor( private route: Router,
            private loginService: LoginService){}

    canActivate(route: ActivatedRouteSnapshot){
        const usuario = this.loginService.usuarioData;
        if(usuario){
            return true;
        }
        this.route.navigate(['/login']);
        return false;
    }
}


