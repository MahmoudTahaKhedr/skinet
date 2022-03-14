import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit{
  title = 'Skinet';
  products: IProduct[];

  
  constructor(private http: HttpClient) {
    
    
  }
  ngOnInit(){
    this.http.get('http://localhost:5000/api/products').subscribe(
      (Response: IPagination) => {
      this.products = Response.data;
  },error =>{
    console.log(error)
      
    });
  }
}
