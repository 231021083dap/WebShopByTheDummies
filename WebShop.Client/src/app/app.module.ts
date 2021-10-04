import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginComponent } from './login/login.component';
import { CategoryComponent } from './category/category.component'; 
import { ProductComponent } from './product/product.component';
import { AdminCategoryComponent } from './admin/category/category.component';
import { AdminProductComponent } from './admin/product/product.component';
import { ProductImageComponent } from './product-image/product-image.component';
import { CartComponent } from './cart/cart.component';
import { ItemComponent } from './item/item.component';
import { AdminCustomerComponent } from './admin/customer/customer.component';
import { CategoryProductsComponent } from './category-products/category-products.component';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    LoginComponent,
    CategoryComponent,
    ProductComponent,
    ProductImageComponent,
    CartComponent,
    AdminCategoryComponent,
    AdminProductComponent,
    ItemComponent,
    AdminCustomerComponent,
    CategoryProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
