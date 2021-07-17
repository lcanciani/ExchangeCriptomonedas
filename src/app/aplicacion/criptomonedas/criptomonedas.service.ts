import { CriptomonedaModel } from "./criptomoneda.model"
import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import {Respuesta} from "src/app/modelosGral/respuesta.modelGral"

const httpOptions = {
    header: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
};


@Injectable({
    providedIn: 'root'
})
export class CriptomonedasService {
    listCriptomonedas:CriptomonedaModel[];
    url:string ="https://localhost:44383/api/Criptomonedas";
    private criptoModel:CriptomonedaModel ;
    private _editarCriptomoneda = new BehaviorSubject<CriptomonedaModel>({}as any);

    constructor(private _http:HttpClient){


    }

    getCriptomonedas(): Observable<Respuesta>{
        return this._http.get<Respuesta>(this.url);
    }
    getCriptomonedaById(id: number):Observable<Respuesta>{
        
        
        return this._http.get<Respuesta>(`${this.url}/${id}`);
    }

    addCriptomoneda(cripto: CriptomonedaModel): Observable<Respuesta>{
       
       return this._http.post<Respuesta>(this.url, cripto);
    }

    editarCriptomoneda(cripto: CriptomonedaModel): Observable<Respuesta>{
        return this._http.put<Respuesta>(this.url, cripto);
    }

    eliminarCriptomoneda(id: number):Observable<Respuesta>{
        return this._http.delete<Respuesta>(`${this.url}/${id}`);
    }

    cargarCripto(element: CriptomonedaModel){
        
        this._editarCriptomoneda.next(element);
      }
    
      editarCripto():Observable<CriptomonedaModel>{
        return this._editarCriptomoneda.asObservable();
      }
}