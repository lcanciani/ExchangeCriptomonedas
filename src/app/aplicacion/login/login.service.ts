import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import {RespuestaAuth} from './respuestaAuth.model';
import { LoginModel } from './login.model';
import {map} from 'rxjs/operators';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';

@Injectable({
    providedIn: 'root'
})



export class LoginService{
    url: string = 'https://localhost:44383/api/Login/login';
    
    private loginSubject: BehaviorSubject<RespuestaAuth>;

    public get usuarioData():RespuestaAuth{
        return this.loginSubject.value;
    }
    constructor( private _http: HttpClient){

        this.loginSubject = new BehaviorSubject<RespuestaAuth>(JSON.parse(localStorage.getItem('usuario')));
    }


    getToken(resp: LoginModel): Observable<Respuesta>{
        return  this._http.post<Respuesta>(this.url, resp ).pipe(
            map(res =>{
                if(res.exito ===1){
                    const user:RespuestaAuth = res.data;
                    localStorage.setItem('usuario',JSON.stringify(user));
                    console.log(localStorage.getItem('usuario'));
                    this.loginSubject.next(user);
                }
                return res;
            })
        );
        
    }

    logout(){
        localStorage.removeItem('usuario');
        this.loginSubject.next(null);
      }
}