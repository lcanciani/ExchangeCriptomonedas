import {Injectable} from '@angular/core'
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {LoginService} from '../login/login.service';
import { Observable } from 'rxjs';


@Injectable()
export class JwtInterceptors implements HttpInterceptor{

    constructor(private _loguinService: LoginService){}

    

    intercept(req: HttpRequest<any>, next:HttpHandler):Observable<HttpEvent<any>>{
        const usuario = this._loguinService.usuarioData;
        if(usuario){
            req = req.clone({
                setHeaders:{
                    Authorization : `Bearer ${usuario.token}`
                }
            });
        }
        return next.handle(req);
    }
}