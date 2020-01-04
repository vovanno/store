import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AuthenticateService } from 'src/app/Services/authenticate.service';
import { User } from 'src/app/Models/user';
import { from } from 'rxjs';
import { Router } from '@angular/router';
@Component({
  selector: 'app-client-login',
  templateUrl: './client-login.component.html',
  styleUrls: ['./client-login.component.css']
})
export class ClientLoginComponent implements OnInit {

  user = new User();

  constructor(private location: Location,private auth: AuthenticateService, private router: Router) { }

  ngOnInit() {
    
  }

  CloseLogin(){
    this.location.back();
  }

  LogIn(form:User){
    this.user.email= form.email;
    this.user.password = form.password;
    this.user.rememberMe = form.rememberMe;
    this.auth.LogIn(this.user).subscribe((data:any)=>{
      localStorage.setItem('token',data.access_token )
      this.router.navigate(["Main"]);
 
    })
  }
}
