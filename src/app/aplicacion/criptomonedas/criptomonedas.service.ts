import { CriptomonedaModel } from "./criptomoneda.model"
import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, BehaviorSubject, observable } from "rxjs";
import {Respuesta} from "src/app/modelosGral/respuesta.modelGral"
import { filter } from "rxjs/operators";

const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZW1haWwiOi"+
"JmZXJjYW5jaWFuaUBnbWFpbC5jb20gICAgICAgICAgICAgICAgICAgICAgICAgICA"+
"gICIsIm5iZiI6MTYyNzMxOTAxNywiZXhwIjoxNjMyNTAzMDE3LCJpYXQiOjE2Mjcz"+
" MTkwMTd9.zCMBDBsGNyyVia1QCxvFugrQyGFD1Owt8Z3Er3DFphQ";

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