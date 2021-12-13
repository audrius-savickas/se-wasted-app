import {useDispatch, useSelector} from "react-redux"
import {setUserId} from "../actions/user"
import {getUserId} from "../selectors/user"

export const useUser = () => {
  const dispatch = useDispatch()

  return {
    userId: useSelector(getUserId),
    setUserId: ({userId}: {userId: string}) => dispatch(setUserId({userId}))
  }
}
