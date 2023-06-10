import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './account/login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";
import { ToastNoAnimationModule, ToastrModule } from 'ngx-toastr';
import { AuthGuard } from './guard/auth-guard.service';
import { RegisterComponent } from './account/register/register.component';
import { TokenIntercepterService } from './helper/token.intercepter.service';
import { ProductComponent } from './component/product/product.component';
import { ProductItemComponent } from './component/product/product-item/product-item.component';
import { ProductActionComponent } from './component/product/product-action/product-action.component';
import { ProductDetailComponent } from './component/product/product-detail/product-detail.component';
import { NavbarComponent } from './component/shared/navbar/navbar.component';
import { EditAddProductComponent } from './component/product/product-action/edit-add-product/edit-add-product.component';
import { CartComponent } from './component/cart/cart.component';
import { CheckoutComponent } from './component/cart/checkout/checkout.component';
import { AddCardComponent } from './component/cart/add-card/add-card.component';
import { OrderComponent } from './component/order/order.component';
import { OrderDetailsComponent } from './component/order/order-details/order-details.component';
import { CategoryComponent } from './component/category/category.component';
import { ProductFilter } from './pipe/product.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CarouselModule  } from 'ngx-owl-carousel-o';
import { ErrorInterceptor } from './helper/error.interceptor';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    LoginComponent,
    RegisterComponent,
    ProductComponent,
    ProductItemComponent,
    ProductActionComponent,
    ProductDetailComponent,
    NavbarComponent,
    EditAddProductComponent,
    CartComponent,
    CheckoutComponent,
    AddCardComponent,
    OrderComponent,
    OrderDetailsComponent,
    CategoryComponent,
    ProductFilter
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    CarouselModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7292"],
        disallowedRoutes: []
      }
  }),
  ToastNoAnimationModule.forRoot(),
],
  providers: [AuthGuard,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenIntercepterService,
    multi: true,
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi:true
  },

],

  bootstrap: [AppComponent]
})
export class AppModule { }
