import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './guard/auth-guard.service';
import { HomepageComponent } from './homepage/homepage.component';
import { RegisterComponent } from './account/register/register.component';
import { ProductDetailComponent } from './component/product/product-detail/product-detail.component';
import { ProductResolver } from './resolve/product.resolve';
import { ProductActionComponent } from './component/product/product-action/product-action.component';
import { EditAddProductComponent } from './component/product/product-action/edit-add-product/edit-add-product.component';
import { CartComponent } from './component/cart/cart.component';
import { CheckoutComponent } from './component/cart/checkout/checkout.component';
import { AddCardComponent } from './component/cart/add-card/add-card.component';
import { OrderComponent } from './component/order/order.component';
import { OrderDetailsComponent } from './component/order/order-details/order-details.component';
import { CategoryComponent } from './component/category/category.component';

const routes: Routes = [
  { path: '', component: HomepageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path : 'category' , component:CategoryComponent},
  { path: 'product', component:ProductActionComponent},
  { path: 'product/add', component:EditAddProductComponent},
  { path: 'product/:id', component:ProductDetailComponent, resolve:{data:ProductResolver}},
  { path: 'product/:id/update', component: EditAddProductComponent, resolve:{data:ProductResolver} },
  { path : 'cart' , component:CartComponent,canActivate:[AuthGuard]},
  { path : 'checkout',component:CheckoutComponent},
  { path : 'addCard', component:AddCardComponent},
  { path : 'orders', component:OrderComponent},
  { path : 'userorder',component:OrderDetailsComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
