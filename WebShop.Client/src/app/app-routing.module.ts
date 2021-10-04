import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminCategoryComponent } from './admin/category/category.component';
import { AdminProductComponent } from './admin/product/product.component';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ItemComponent } from './item/item.component';
import { LoginComponent } from './login/login.component';
import { Role } from './models';
import { ProductComponent } from './product/product.component';
import { AuthGuard } from './_helpers/auth.guard';
import { AdminCustomerComponent } from './admin/customer/customer.component';
import { CategoryProductsComponent } from './category-products/category-products.component';
import { CartComponent } from './cart/cart.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'Admin/Category', component: AdminCategoryComponent}, // canActivate: [AuthGuard], data: { roles: [Role.Admin]} },
  { path: 'Category', component: CategoryComponent},
  { path: 'Product', component: ProductComponent},
  { path: 'item/:itemId', component: ItemComponent },
  { path: 'Admin/Product', component: AdminProductComponent}, 
  { path: 'Login', component: LoginComponent },
  { path: 'Admin/Customer', component: AdminCustomerComponent},
  { path: 'Category/Products/:categoryId', component:CategoryProductsComponent }, 
  { path: 'Cart', component:CartComponent}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
