import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { CartService } from 'src/app/service/cart.service';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent {
  myForm:FormGroup
  userId:string
  constructor( private fb:FormBuilder, private orderService:OrderService, private auth:AuthenticationService){}
  ngOnInit(): void {
    this.userId = this.auth.getUserId()
    this.myForm = this.fb.group({
      cardName: [''],
      cardNumber:[],
      expiryMonth:[],
      expiryYear:[],
      cvv:['']
    })

    
  }

  addCard(){
    this.orderService.addCard(this.userId,this.myForm.value)
  }
}
