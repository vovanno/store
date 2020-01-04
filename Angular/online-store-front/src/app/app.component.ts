import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'online-store-front';
  rights:string = "@2007-2010 Vivrocks.com | All Rights Reserved"
  additionalInfo: string = "PC Magazine Editors' Choice Award Logo is a registered tradeark of Zeff Davis Publishing Holdings, inc."

  constructor(private router: Router){}

  ClientLogin(){
    this.router.navigate(['ClientLogin']);
  }

  Supportlogin(){
    this.router.navigate(['SupportLogin']);
  }
}
