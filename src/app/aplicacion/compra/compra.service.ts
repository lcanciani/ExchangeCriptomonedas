import {CriptomonedaModel} from '../criptomonedas/criptomoneda.model';
import {BehaviorSubject, Observable} from 'rxjs';
import {Injectable,OnInit} from '@angular/core';
import {CriptomonedasService} from '../criptomonedas/criptomonedas.service'
import {ConfigurarCompraModel} from './configurar-compra/cofigurarCompra.model';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient} from "@angular/common/http";
import { Router } from '@angular/router';


import { ConfirmarCompraModel } from './confirmar-compra/confirmarCompra.model';

@Injectable({
    providedIn: 'root'
})
export class CompraService implements OnInit{
private    _subjectCriptoCompra=new BehaviorSubject<Respuesta>({}as any);
private    _subjectConfirmarCompra=new BehaviorSubject<ConfirmarCompraModel>({}as any);
private _url: string = "https://localhost:44383/api/Venta";
private _confirmarCompraModel: ConfirmarCompraModel;
constructor(private _criptoService:CriptomonedasService,
            private _http: HttpClient,
            private _route: Router){}

ngOnInit(){
    
}

getCriptoCompraById(idCripto: number): Observable<Respuesta>{
return this._http.get<Respuesta>(`${this._url}/${idCripto}`);
}

configurarCompra(idCripto: number){
    console.log(idCripto)
    this.getCriptoCompraById(idCripto).subscribe(resp =>{
        console.log('service')
        console.log(resp)

       this._subjectCriptoCompra.next(resp);
       this._route.navigate(['/comp/configurarCompra']);
    })
    
}

getCriptoAConfigurarCompra():Observable<Respuesta>{
    return this._subjectCriptoCompra.asObservable();

}



confirmarCompra(conf: ConfirmarCompraModel){
    this._confirmarCompraModel = conf;
    this._subjectConfirmarCompra.next(conf);
       this._route.navigate(['/comp/confirmarCompra']);
    
}

getConfirmarCompra():Observable<ConfirmarCompraModel>{
    return this._subjectConfirmarCompra.asObservable();

}
insertarCompra():Observable<Respuesta>{
console.log('llegue CompraService.insertarCompra()')
console.log(this._confirmarCompraModel)
 return this._http.post<Respuesta>(`${this._url}`,this._confirmarCompraModel);
}
}