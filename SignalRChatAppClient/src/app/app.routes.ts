import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { SignupComponent } from './components/signup/signup.component';

export const routes: Routes = [
    {
        path: "",
        component: LayoutComponent,
        canActivateChild:[() => inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "",
                component: HomeComponent
            }
        ]
    },
    {
        path: "signup",
        component: SignupComponent
    },
    {
        path: "login",
        component: LoginComponent
    }
];
