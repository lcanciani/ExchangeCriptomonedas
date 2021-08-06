import { CriptomonedaModel } from "./criptomoneda.model"
import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import {Respuesta} from "src/app/modelosGral/respuesta.modelGral"




const httpOptions = {
    headers: new HttpHeaders({
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

    constructor(private _http:HttpClient){}

   
    actualizarLista(criptos: CriptomonedaModel[]){
        this.listCriptomonedas = criptos;
    }

    getCriptomonedas(): Observable<Respuesta>{
        return this._http.get<Respuesta>(`${this.url}`, httpOptions);
    }
    getCriptomonedaById(id: number):Observable<Respuesta>{
        
        
        return this._http.get<Respuesta>(`${this.url}/${id}`, httpOptions);
    }

    addCriptomoneda(cripto: CriptomonedaModel): Observable<Respuesta>{
       
       return this._http.post<Respuesta>(this.url, cripto, httpOptions);
    }

    editarCriptomoneda(cripto: CriptomonedaModel): Observable<Respuesta>{
        return this._http.put<Respuesta>(this.url, cripto, httpOptions);
    }

    eliminarCriptomoneda(id: number):Observable<Respuesta>{
        return this._http.delete<Respuesta>(`${this.url}/${id}`, httpOptions);
    }

    cargarCripto(element: CriptomonedaModel){
        
        this._editarCriptomoneda.next(element);
      }
    
      editarCripto():Observable<CriptomonedaModel>{
        return this._editarCriptomoneda.asObservable();
      }
}