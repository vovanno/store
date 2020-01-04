import { Injectable } from '@angular/core';
import { Platform } from '../Models/platform';
import { HttpClient } from '@angular/common/http';
import { extendedPlatform } from '../Models/extendedPlatform';

@Injectable({
  providedIn: 'root'
})
export class PlatformService {
  private Url = "https://localhost:44326";

  platformData: Platform[];
  extendedPlatform: extendedPlatform[];
  
  constructor(private http: HttpClient) { }

  public GetPlatforms(){
    return this.http.get(this.Url + "/api/platform");
  }
}
