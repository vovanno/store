import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  constructor(private http: HttpClient) { }

  private Url = "https://localhost:44326";

  LogIn(user:User){
    var json = JSON.stringify(user);
    var reqHeader = new HttpHeaders({'Content-Type':'application/json', 'No-Auth':'True'});
    return this.http.post(this.Url + "/api/Authentication/login",json,{headers:reqHeader});
  }
}
