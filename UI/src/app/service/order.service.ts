import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { apiServer } from 'src/assets/config/config';
import { CartService } from './cart.service';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';
import { catchError, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  url = apiServer.URL + "api/orders"

  constructor(private http:HttpClient,private authService:AuthenticationService, private toastr:ToastrService, private cartService:CartService, private router:Router) { }

  getCard(id:string, data:any , email:string, total:number){
    this.http.post(this.url + `/card/${id}`, Object.assign({},{userId:id}, data)).subscribe(()=>{
      this.addOrder(id , email , total)
    })
  }

  addOrder(uid:string,email:string,total:number){
    var usercart = this.cartService.getCartItems();
    var user = {userId:uid , userName:email, total:total , cart:usercart};
    this.http.post(this.url + '/addOrder', user).subscribe(()=>{
      this.cartService.clearCart()      
      this.toastr.success("added successfully")

      this.router.navigate(['/'])
    });
  }

   getOrderDetails(){
    const id =  this.authService.getUserId();
    return this.http.get(`${this.url}/orderuser/${id}`)
  }

  addCard(uid:string, data:any){
    this.http.post(this.url + `/addcard`, Object.assign({},{userId:uid}, data), { headers:new HttpHeaders({'Content-Type': 'application/json'}) }).subscribe(d=>{
      this.toastr.success("user card added successfully")
    })
  }

  getOrders(id:string){
    return this.http.get(this.url + `/orders/${id}`)
  }


}
