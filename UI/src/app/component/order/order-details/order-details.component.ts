import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit{
  userOrder!:any
  constructor(private orderService:OrderService){}
  ngOnInit(): void {
    this.orderService.getOrderDetails().subscribe(d=>{
      this.userOrder = d
      
    })
  }
}
