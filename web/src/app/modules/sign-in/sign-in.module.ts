import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignInRoutingModule } from './sign-in-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { SignInComponent } from './pages/login/sign-in.component';

@NgModule({
    declarations: [SignInComponent],
    imports: [
        CommonModule,
        FormsModule,
        SignInRoutingModule,
        ReactiveFormsModule,
        SharedModule
    ],
})
export class SignInModule {}
