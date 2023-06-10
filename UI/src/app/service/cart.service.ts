import { Injectable } from '@angular/core';
import { Product } from '../model/porduct.model';
import { Cart } from '../model/cart';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartKey = "cart"
  cartChanges = new Subject<Cart[]>();
  cartCount = new Subject<number>();
  constructor() { }

  addToCart(item:Cart){
    const cartItems = this.getCartItems();
    const existingItem = cartItems.find(cartItem=>cartItem.productId == item.productId );
    if(existingItem){
      existingItem.qty += item.qty
    }else{
      item.qty=1;
      cartItems.push(item)
    }

    this.updateCartItems(cartItems)
  }


  increaseItem(item:Cart){
    const cartItems = this.getCartItems();
    const cartItem = cartItems.find(x=>x.productId === item.productId);
    if(cartItem){
      cartItem.qty += 1
    }
    this.updateCartItems(cartItems)
  }

  decreaseItem(item:Cart){
    const cartItems = this.getCartItems();
    const cartItem = cartItems.find(x=>x.productId === item.productId);
    cartItem.qty -= 1
    if( cartItem.qty < 1){
        this.removeFromCart(item)
    }else{
      this.updateCartItems(cartItems)
    }
  }

  getCartItems():Cart[]{
    const cart = localStorage.getItem(this.cartKey);
    return cart ? JSON.parse(cart): [];
  }

  removeFromCart(item:any){
    const cartItems = this.getCartItems()
    const updatedItem = cartItems.filter(x=> x.productId !== item.productId)
     this.updateCartItems(updatedItem)

  }

  updateCartItems(items:any[]){
    localStorage.setItem(this.cartKey,JSON.stringify(items))   
    this.emitCart(items)
    this.emitCartNumber()
  }

  clearCart(){
    localStorage.removeItem(this.cartKey)
    this.emitCartNumber()

  }

  
getTotal(){
  return this.getCartItems().reduce((total,item)=> total + item.price * item.qty , 0)
}

cartNumber(){
  return this.getCartItems().reduce((ele)=> ele + 1,0)
}
emitCartNumber(){
  let cart = this.cartNumber();
  cart = cart > 0 ? cart : null 
   this.cartCount.next(cart) 
}
emitCart(items:Cart[]){
  this.cartChanges.next(this.getCartItems())
  this.getTotal()
}



}
