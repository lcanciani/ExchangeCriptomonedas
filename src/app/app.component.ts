import { Component , HostListener} from '@angular/core';
import { LoginService } from './aplicacion/login/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})



export class AppComponent {
  title = 'ExchangeFrontend';

  constructor(private _loginService: LoginService){}


  @HostListener('window:unload', [ '$event' ])
//activar para que cuando se cierre la ventana/pestaña se borre la session
  unloadHandler(event) {
    //this._loginService.logout();
   }
  
}
