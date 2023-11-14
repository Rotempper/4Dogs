// קומפוננטות
import { AppComponent } from './app.component';
import { NavbarComponent } from './Screens/navbar/navbar.component';
import { HeaderComponent } from './Layout/header/header.component';
import { HomePageComponent } from './Screens/home-page/home-page.component';
import { FooterComponent } from './Layout/footer/footer.component';
import { LoginComponent } from './Screens/login/login.component';
import { CreateUserComponent } from './Screens/create-user/create-user.component';
import { ShopsComponent } from './Screens/shops/shops.component';
import { PensionComponent } from './Screens/pension/pension.component';
import { TrainingComponent } from './Screens/training/training.component';
import { BarbershopComponent } from './Screens/barbershop/barbershop.component';
import { AboutComponent } from './Screens/about/about.component';
import { OrdersComponent } from './Screens/orders/orders.component';
import { DialogLogoutComponent } from './dialog-logout/dialog-logout.component';
import { TermsComponent } from './Screens/terms/terms.component';
import { TheDiaryComponent } from './Screens/the-diary/the-diary.component';
import { AdminComponent } from './Screens/admin/admin.component';
import { EditShopAdminComponent } from './Screens/editShop-admin/editShop-admin.component';
import { AddComponent } from './Screens/addShop-Admin/addShop-Admin.component';
import { UserDataComponent } from './Screens/user-data/user-data.component';
import { EditUsersComponent } from './Screens/edit-users/edit-users.component';
import { AddUsersComponent } from './Screens/add-users/add-users.component';

// הוספת קומפוננטות עיצוב
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { MatExpansionModule} from '@angular/material/expansion';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatIconModule} from '@angular/material/icon';
import { MatFormFieldModule} from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatRadioModule} from '@angular/material/radio';
import { MatSelectModule} from '@angular/material/select';
import { MatProgressBarModule} from '@angular/material/progress-bar';
import { MatCardModule} from '@angular/material/card';
import { MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatInputModule} from '@angular/material/input';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { MatPaginatorModule} from '@angular/material/paginator';
import { MatTabsModule} from '@angular/material/tabs';
import { MatDialogModule} from '@angular/material/dialog';
import { TimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { MatTableModule} from '@angular/material/table';
import { MatTooltipModule} from '@angular/material/tooltip';
import { NgbCalendar, NgbCalendarHebrew, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSortModule} from '@angular/material/sort';
import { AdminOrderManagementComponent } from './Screens/admin-order-management/admin-order-management.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HeaderComponent,
    HomePageComponent,
    FooterComponent,
    LoginComponent,
    CreateUserComponent,
    ShopsComponent,
    PensionComponent,
    TrainingComponent,
    BarbershopComponent,
    AboutComponent,
    OrdersComponent,
    DialogLogoutComponent,
    TermsComponent,
    TheDiaryComponent,
    AdminComponent,
    EditShopAdminComponent,
    AddComponent,
    UserDataComponent,
    EditUsersComponent,
    AddUsersComponent,
    AdminOrderManagementComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    MatExpansionModule,
    MatDatepickerModule,
    MatIconModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    HttpClientModule,
    FormsModule,
    MatRadioModule,
    MatSelectModule,
    MatProgressBarModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatTabsModule,
    MatDialogModule,
    TimePickerModule,
    MatTableModule,
    MatTooltipModule,
    MatSortModule
  ],
  providers: [
    {provide: NgbCalendar, useClass: NgbCalendarHebrew},
    MatDatepickerModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
