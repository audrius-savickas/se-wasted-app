import {createAction} from "@reduxjs/toolkit"

export const setRestaurantId = createAction<{id: string}>("SET_RESTAURANT_ID")
