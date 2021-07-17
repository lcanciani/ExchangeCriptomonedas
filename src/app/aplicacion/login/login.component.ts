import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { RespuestaAuth } from './respuestaAuth.model';
import { Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators  } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm : FormGroup;
  token: string;
  respu : RespuestaAuth = null;
  constructor( private _loginService: LoginService,
               private _route: Router,
               private _formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      email:[''],
      password:['']
    })

  }

  getUserToken(){
    
    this._loginService.getToken(this.loginForm.value).subscribe(resp => {
      if(resp.exito ===1){
       // console.log(resp.data.token)
        this._route.navigate(['comp']);
      }
    })
  }

 
}
