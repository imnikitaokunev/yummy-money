import { environment } from './../../../environments/environment';
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

    public getExpensesEndpoint(): string {
        return this.createUrlWithQueryParameters(
            'expenses',
            (qs: QueryStringParameters) => {
                qs.push('includes', 'Category');
            }
        );
    }

    public getIncomesEndpoint(): string {
        return this.createUrlWithQueryParameters(
            'incomes',
            (qs: QueryStringParameters) => {
                qs.push('includes', 'Category');
            }
        );
    }
}
