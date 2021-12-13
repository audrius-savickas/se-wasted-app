import {createAction} from "@reduxjs/toolkit"

export const setUserId = createAction<{userId: string}>("SET_USER_ID")
