import {CriptomonedaModel} from '../criptomonedas/criptomoneda.model';
import {BehaviorSubject, Observable} from 'rxjs';
import {Injectable,OnInit} from '@angular/core';
import {CriptomonedasService} from '../criptomonedas/criptomonedas.service'
import {ConfiguracionVenta} from './configurar-venta/cofiguracionVenta.model';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { Router } from '@angular/router';
import { ConfirmarVentaComponent } from './confirmar-venta/confirmar-venta.component';
import { ConfirmarVentaModel } from './confirmar-venta/confirmarVenta.model';

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
  };

@Injectable({
    providedIn: 'root'
})
export class VentaService implements OnInit{
private    _subjectCriptoVenta=new BehaviorSubject<Respuesta>({}as any);
private    _subjectConfirmarVenta=new BehaviorSubject<ConfirmarVentaModel>({}as any);
private _url: string = "https://localhost:44383/api/Compra";
private _urlError: string = "https://localhost:44383/api/Error";
private _confirmarVentaModel: ConfirmarVentaModel;
constructor(private _criptoService:CriptomonedasService,
            private _http: HttpClient,
            private _route: Router){}

ngOnInit(){
    
}

getCriptoVentaById(idCripto: number): Observable<Respuesta>{
return this._http.get<Respuesta>(`${this._url}/${idCripto}`, httpOptions);
}

configurarVenta(idCripto: number){
    
    this.getCriptoVentaById(idCripto).subscribe(resp =>{
        

       this._subjectCriptoVenta.next(resp);
       this._route.navigate(['/comp/configurarVenta']);
    })
    
}

getCriptoAConfigurarVenta():Observable<Respuesta>{
    return this._subjectCriptoVenta.asObservable();

}



confirmarVenta(conf: ConfirmarVentaModel){
    this._confirmarVentaModel = conf;
    this._subjectConfirmarVenta.next(conf);
       this._route.navigate(['/comp/ConfirmarVenta']);
    
}

getConfirmarVenta():Observable<ConfirmarVentaModel>{
    return this._subjectConfirmarVenta.asObservable();

}
insertarVenta():Observable<Respuesta>{

 return this._http.post<Respuesta>(`${this._url}`,this._confirmarVentaModel, httpOptions);
}

estadoPrecioCompra(): Observable<Respuesta>{
    return this._http.get<Respuesta>(`${this._urlError}`, httpOptions)
}

}