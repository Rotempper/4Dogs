import { AddUsersComponent } from './Screens/add-users/add-users.component';
import { AdminComponent } from './Screens/admin/admin.component';
import { OrdersComponent } from './Screens/orders/orders.component';
import { AboutComponent } from './Screens/about/about.component';
import { BarbershopComponent } from './Screens/barbershop/barbershop.component';
import { TrainingComponent } from './Screens/training/training.component';
import { PensionComponent } from './Screens/pension/pension.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './Screens/create-user/create-user.component';
import { HomePageComponent } from './Screens/home-page/home-page.component';
import { LoginComponent } from './Screens/login/login.component';
import { ShopsComponent } from './Screens/shops/shops.component';
import { TermsComponent } from './Screens/terms/terms.component';
import { TheDiaryComponent } from './Screens/the-diary/the-diary.component';
import { EditShopAdminComponent } from './Screens/editShop-admin/editShop-admin.component';
import { UserDataComponent } from './Screens/user-data/user-data.component';
import { AdminOrderManagementComponent } from './Screens/admin-order-management/admin-order-management.component';

const routes: Routes = [

  {path:'Training', component:TrainingComponent },
  {path:'Orders', component:OrdersComponent },
  {path:'About', component:AboutComponent },
  {path:'Barbershop', component:BarbershopComponent },
  {path:'CreateUser',component:CreateUserComponent},
  {path:'Shops',component:ShopsComponent},
  {path:'Pension',component:PensionComponent},
  {path:'Login',component:LoginComponent},
  {path:'Home',component:HomePageComponent},
  {path:'Terms',component:TermsComponent},
  {path:'TheDiary',component:TheDiaryComponent},
  {path:'Admin',component:AdminComponent},
  {path:'EditShopAdmin',component:EditShopAdminComponent},
  {path:'user-data',component:UserDataComponent},
  {path:'add-users',component:AddUsersComponent},
  {path:'admin-order-management',component:AdminOrderManagementComponent},
  {path: '**', component:HomePageComponent }, // ברירת מחדל
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
