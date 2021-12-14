import {createAction} from "@reduxjs/toolkit"

export const setCustomerId = createAction<{customerId: string}>("SET_CUSTOMER_ID")
