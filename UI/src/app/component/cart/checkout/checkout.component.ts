import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cart } from 'src/app/model/cart';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { CartService } from 'src/app/service/cart.service';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit{
  cart:Cart[];
  total:number;
  myForm:FormGroup
  userId:string
  constructor(private cartService:CartService, private fb:FormBuilder, private orderService:OrderService, private auth:AuthenticationService){}
  ngOnInit(): void {
    this.cart = this.cartService.getCartItems();
    this.total = this.cartService.getTotal()
    this.userId = this.auth.getUserId()
    this.myForm = this.fb.group({
      cardName: ['', Validators.required],
      cardNumber:[undefined, Validators.required],
      expiryMonth:[undefined, Validators.required],
      expiryYear:[undefined, Validators.required],
      cvv:['', Validators.required]
    })
  }

  CheckOut(){
    if(!this.myForm.invalid)
      this.orderService.getCard(this.userId,this.myForm.value , this.auth.getUserEmail(), this.cartService.getTotal())
  }
}
