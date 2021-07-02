import { CriptomonedaModel } from "./criptomoneda.model"
import {Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";



@Injectable({
    providedIn:'root'
})
export class UsuarioService{

    listCriptomonedas:CriptomonedaModel[];
    url:string ="https://localhost:44383/api/Criptomonedas ";

    constructor(private http:HttpClient){


    }

    getCriptomonedas(): Observable<CriptomonedaModel[]>{
        return this.http.get<CriptomonedaModel[]>(this.url);
    }

}