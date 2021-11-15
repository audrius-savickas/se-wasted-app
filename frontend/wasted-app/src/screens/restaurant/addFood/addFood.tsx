import React, { useEffect, useState } from "react"
import { Navigation } from "react-native-navigation"
import { Text, View, Button, Wizard, Colors } from "react-native-ui-lib"
import { setHomeRoot } from "../../../services/navigation"
import { AddFoodScreenProps } from "./interfaces"
import { BaseInfo } from "./Wizard.Steps/baseInfo"
import { FinalStep } from "./Wizard.Steps/finalStep"
import { PriceDecreasing } from "./Wizard.Steps/priceDecreasing"

const completedStepIndex = 3

export const AddFood = ({} : AddFoodScreenProps) => {
  const [activeIndex, setActiveIndex] = useState<number>(0)

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])  

  const getStepState = (n: Number) => {
    if (n===activeIndex) return Wizard.States.ENABLED

    return (n>activeIndex)
      ? Wizard.States.DISABLED 
      : Wizard.States.COMPLETED
  }

  const renderCurrentStep = () => {
    return (
      <View flex left marginT-10 marginL-10
            style={{
              flexDirection: "column",
              justifyContent: "center"
            }}
      >
        {renderStep(activeIndex)}
      </View>
    )
  }

  const renderStep = (n: number) => {
    switch(n) {
      case 0:
        return (
          <>
            <BaseInfo />
          </>
        )
      case 1:
        return (
          <>
            <PriceDecreasing />
          </>
        )
      case 2:
        return (
          <>
            <FinalStep />
          </>
        )
    }
  }

  return (
      <View flex margin-s3>
        <Text center text40M marginT-10>Add new meal</Text>
        <Wizard testID={'addFood.wizard'} activeIndex={activeIndex}>
          <Wizard.Step state={getStepState(0)} label={'Fill basic information'}/>
          <Wizard.Step state={getStepState(1)} label={'Fill price decreasing'}/>
          <Wizard.Step state={getStepState(2)} label={'Add food'}/>
        </Wizard>

        <View style={{height: '70%'}} >
          {renderCurrentStep()}
        </View>

        <View 
            flex
            style={{
              flexDirection: "row",
              alignItems: 'flex-end',
              width: '100%',
              height: '30%',
              justifyContent: 'space-evenly'
            }}
        >
          <Button
            backgroundColor={Colors.blue10}
            label="Previous step"
            disabled={activeIndex===0}
            onPress={() => setActiveIndex(activeIndex-1)}
          />
          <Button
            backgroundColor={Colors.green10}
            label="Next"
            disabled={activeIndex===completedStepIndex}
            onPress={() => setActiveIndex(activeIndex+1)}
          />
        </View>
      </View>
  )
}
