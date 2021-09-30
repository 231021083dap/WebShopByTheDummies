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
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  { path: '', component: FrontpageComponent },

  { path: 'Category', component: CategoryComponent},
  { path: 'AdminCategory', component: AdminCategoryComponent},

  { path: 'Product', component: ProductComponent},
  { path: 'item/:itemId', component: ItemComponent },
  { path: 'AdminProduct', component: AdminProductComponent},

  { path: 'Register', component: RegisterComponent},

  { path: 'Login', component: LoginComponent },

//  { path: 'admin/authors', component: AuthorComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
