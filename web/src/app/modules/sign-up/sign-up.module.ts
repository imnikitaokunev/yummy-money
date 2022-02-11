import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignUpRoutingModule } from './sign-up-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [SignUpComponent],
    imports: [
        CommonModule,
        FormsModule,
        SignUpRoutingModule,
        ReactiveFormsModule,
        SharedModule,
    ],
})
export class SignUpModule {}
