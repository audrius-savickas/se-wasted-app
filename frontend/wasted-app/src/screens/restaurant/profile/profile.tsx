import React, { useEffect, useState } from "react"
import { Navigation } from "react-native-navigation"
import {Button, Colors, LoaderScreen, Text, View, TextField, Image} from "react-native-ui-lib"
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { getRestaurantById, updateRestaurant as updateRestaurantCall } from "../../../api"
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
    imageURL: "",
    distanceToUser: 0.0
  })
  const [updatedRestaurant, setUpdatedRestaurant] = useState<Restaurant>(restaurant)
  const [loading, setLoading] = useState(true)
  const [isSaveButtonDisabled, setIsSaveButtonDisabled] = useState(false)

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
    
    if (response !== null) {
      setRestaurant(response)
      setUpdatedRestaurant(response)
    }

    setLoading(false)
  }

  const updateRestaurant = async () => {
    setIsSaveButtonDisabled(true)
    await updateRestaurantCall(updatedRestaurant)
    setIsSaveButtonDisabled(false)
  }

  const onChangeName = (name : string) => {
   setUpdatedRestaurant({
      ...updatedRestaurant,
      name
    }) 
  }

  const onChangeAddress = (address : string) => {
    setUpdatedRestaurant({
      ...updatedRestaurant,
      address
    })
  }

  const cancelUpdate = () => {
    setUpdatedRestaurant(restaurant)
  }

  return (
    <View flex margin-s3>
        {loading 
          ? 
          (
            <View flex centerH>
              <LoaderScreen color={Colors.blue30} message="Loading..." />
            </View>
          ) 
          : 
          (
            <View flex margin-s3>
              <View flex center>
                <Text center text40M marginT-10>My profile</Text>
                <Image
                  marginT-s2
                  source={{
                    uri: restaurant.imageURL, 
                    height: 200, 
                    width: 330
                  }} 
                />
              </View>
              <View flex left marginT-10
                style={{
                  flexDirection: "column",
                  justifyContent: "center"
                }}
              >
                <TextField
                  title="Name"
                  onChangeText={onChangeName}
                  value={updatedRestaurant.name}
                  textFieldCommonValues
                  />
                <TextField
                  title="Location"
                  onChangeText={onChangeAddress}
                  value={updatedRestaurant.address}  
                  textFieldCommonValues 
                  />
                  </View>
                <Button
                  outline
                  label="Change Password"
                />
                <View 
                  flex
                  style={{
                    flexDirection: "row",
                    alignItems: 'center',
                    justifyContent: 'space-evenly',
                  }}
                >
                  <Button
                    link
                    label="Reset"
                    onPress={cancelUpdate}
                  />
                  <Button
                    label="Save"
                    disabled={isSaveButtonDisabled}
                    onPress={updateRestaurant}
                  />
                </View>
            </View>
          )
        }
    </View>
  )
}
