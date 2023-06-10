import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  uId:string
  orders:any;
constructor(private orderService:OrderService, private auth:AuthenticationService){}
ngOnInit(): void {
  this.uId = this.auth.getUserId()
  this.orderService.getOrders(this.uId).subscribe(d=>{
    this.orders = d
  })
}


}
