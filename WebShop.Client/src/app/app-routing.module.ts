import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

import { LoginComponent } from './login/login.component';
import { Role } from './models';
import { ProductComponent } from './product/product.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'category', component: CategoryComponent},
  { path: 'product', component: ProductComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'login', component: LoginComponent },
//  { path: 'admin/authors', component: AuthorComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
