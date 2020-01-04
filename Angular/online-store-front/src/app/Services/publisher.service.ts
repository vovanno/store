import { Injectable } from '@angular/core';
import { Publisher } from '../Models/publisher';
import { HttpClient } from '@angular/common/http';
import { extendedPublisher } from '../Models/extendedPublisher';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {
  private Url = "https://localhost:44326";

  publisherData: Publisher[];
  extendedPublisher: extendedPublisher[];
  
  constructor(private http: HttpClient) { }

  public GetPublishers(){
    return this.http.get(this.Url+"/api/publisher");
  }
}
