import { AuthGuard } from './core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full',
    },
    {
        path: 'home',
        loadChildren: () =>
            import('./modules/home/home.module').then((m) => m.HomeModule),
    },
    {
        path: 'transactions',
        loadChildren: () =>
            import('./modules/transactions/transactions.module').then(
                (m) => m.TransactionsModule
            ),
        canActivate: [AuthGuard],
    },
    {
        path: 'list',
        loadChildren: () =>
            import('./modules/list/list.module').then((m) => m.ListModule),
        canActivate: [AuthGuard],
    },
    {
        path: 'signin',
        loadChildren: () =>
            import('./modules/sign-in/sign-in.module').then(
                (m) => m.SignInModule
            ),
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
