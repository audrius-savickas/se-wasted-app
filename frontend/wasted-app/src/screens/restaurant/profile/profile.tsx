import React, { useEffect, useState } from "react"
import { Navigation } from "react-native-navigation"
import {Button, Colors, LoaderScreen, Text, View, TextField} from "react-native-ui-lib"
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { getRestaurantById } from "../../../api"
import { Restaurant } from "../../../api/interfaces"
import { setHomeRoot } from "../../../services/navigation"
import { ProfileProps } from "./interfaces"

const textFieldCommonValues : TextFieldProps = {
  editable:false,
  centered: false
}

export const Profile = ({
  restaurantId,
  restaurantName
} : ProfileProps ) => {
  
  const [restaurant, setRestaurant] = useState<Restaurant>({
    id: restaurantId,
    name: restaurantName,
    coords: {
      latitude: 0.0,
      longitude: 0.0
    },
    address: "",
  })
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    fetchRestaurantInfo()

    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  const fetchRestaurantInfo = async () => {
    const response = await getRestaurantById(restaurantId)
    
    if (response != null)
      setRestaurant(response)

    setLoading(false)
  }

  return (
    <View flex centerH>
        {loading 
          ? 
          (
              <LoaderScreen color={Colors.blue30} message="Loading..." />
          ) 
          : 
          (
            <>
              <Text text40M>My profile</Text>
              <TextField 
                title="Name"
                textFieldCommonValues
              />
              <TextField 
                title="Location"  
                textFieldCommonValues 
              />
              <TextField 
                title="Email"  
                textFieldCommonValues
              />
              <Button
                outline
                label="Change Password"
              />
              <Button
                outline
                label="Cancel"
              />
              <Button
                label="Save"
              />
            </>
          )
        }
    </View>
  )
}
