import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { QueryStringParameters } from 'src/app/shared/classes/query-string-parameters';
import { UrlBuilder } from 'src/app/shared/classes/url-builder';

@Injectable()
export class ApiEndpointsService {
    constructor() {}

    /* #region URL CREATOR */
    // UR
    private createUrl(action: string): string {
        const urlBuilder: UrlBuilder = new UrlBuilder(
            environment.apiUrl,
            action
        );
        return urlBuilder.toString();
    }

    // URL WITH QUERY PARAMS
    private createUrlWithQueryParameters(
        action: string,
        queryStringHandler?: (
            queryStringParameters: QueryStringParameters
        ) => void
    ): string {
        const urlBuilder: UrlBuilder = new UrlBuilder(
            environment.apiUrl,
            action
        );
        // Push extra query string params
        if (queryStringHandler) {
            queryStringHandler(urlBuilder.queryString);
        }
        return urlBuilder.toString();
    }

    // URL WITH PATH VARIABLES
    private createUrlWithPathVariables(
        action: string,
        pathVariables: any[] = []
    ): string {
        let encodedPathVariablesUrl: string = '';
        // Push extra path variables
        for (const pathVariable of pathVariables) {
            if (pathVariable !== null) {
                encodedPathVariablesUrl += `/${encodeURIComponent(
                    pathVariable.toString()
                )}`;
            }
        }
        const urlBuilder: UrlBuilder = new UrlBuilder(
            environment.apiUrl,
            `${action}${encodedPathVariablesUrl}`
        );
        return urlBuilder.toString();
    }
    /* #endregion */

    public getTransactionsEndpoint(request: Object): string {
        return this.createUrlWithQueryParameters(
            'transactions',
            (qs: QueryStringParameters) => this.populateParameters(qs, request)
        );
    }

    public getTransactionsPagedEndpoint(request: Object): string {
        return this.createUrlWithQueryParameters(
            'transactions/paged',
            (qs: QueryStringParameters) => this.populateParameters(qs, request)
        );
    }

    public postTransactionEndpoint(): string {
        return this.createUrl('transactions');
    }

    public putTransactionEndpoint(id: number): string {
        return this.createUrlWithPathVariables('transactions', [id]);
    }

    public deleteTransactionEndpoint(id: number): string {
        return this.createUrlWithPathVariables('transactions', [id]);
    }

    public getCategoriesEndpoint(): string {
        return this.createUrl('categories');
    }

    public signInEndpoint(): string {
        return this.createUrl('identity/signin');
    }

    private populateParameters(
        qs: QueryStringParameters,
        request: Object
    ): void {
        for (var property of Object.keys(request)) {
            if (request[property]) {
                qs.push(property, request[property]);
            }
        }
    }
}
