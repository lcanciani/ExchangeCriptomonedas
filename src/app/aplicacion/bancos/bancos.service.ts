import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from '../../modelosGral/respuesta.modelGral';
import { HttpClient } from '@angular/common/http';
import { BancoModel } from './banco.model';


@Injectable({
    providedIn: 'root'
})
export class BancosService{

    private _url: string = 'https://localhost:44383/api/Bancos';
    constructor(private _http: HttpClient){}

    getBancos():Observable<Respuesta>{
        return this._http.get<Respuesta>(this._url);
    }
    
}
